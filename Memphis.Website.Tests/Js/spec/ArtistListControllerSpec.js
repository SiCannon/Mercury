describe("ArtistListController", function () {

    var $controller, $scope, controller, mockArtistService, mockPagingTracker;

    var artists = [
        { ArtistId: 1, Name: "one" },
        { ArtistId: 2, Name: "two" },
        { ArtistId: 3, Name: "three" }
    ];

    var initialPageSize = 44;
    var updatedPageSize = 55;

    beforeEach(module("app"));

    beforeEach(function () {

        module(function ($provide) {
            mockArtistService = jasmine.createSpyObj("mockArtistService", ["query", "count", "get", "save", "delete"]);
            mockArtistService.query.and.callFake(function (pageNumber, pageSize, searchText, success) { success(artists); });
            mockArtistService.count.and.callFake(function (searchText, success) { success(77); });
            mockArtistService.delete.and.callFake(function (id, success) { success(artists); });
            $provide.value("ArtistService", mockArtistService);

            mockPagingTracker = jasmine.createSpyObj("mockPagingTracker", ["init"]);
            mockPagingTracker.init.and.returnValue({ CurrentPage: 3, PageSize: initialPageSize });
            $provide.value("PagingTrackerService", mockPagingTracker);
        });

        inject(function (_$controller_, $rootScope) {
            $controller = _$controller_;
            $scope = $rootScope.$new();
            controller = $controller("ArtistListController", { $scope: $scope });
        });
    });

    describe("initialization", function () {

        it("sets SearchText to an empty string", function () {
            expect($scope.SearchText).toEqual("");
        });

        it("sets paging to the correct instance of PagingInfo", function () {
            expect(mockPagingTracker.init).toHaveBeenCalled();
            expect(mockPagingTracker.init).toHaveBeenCalledWith("artist");
            expect($scope.Paging).toBeDefined();
            expect($scope.Paging.CurrentPage).toEqual(3);
            expect($scope.Paging.PageSize).toEqual(initialPageSize);
        });

        it("assigns getData in paging", function () {
            expect($scope.Paging.getData).toBeDefined();
            expect($scope.Paging.getData).toEqual(jasmine.any(Function));
        });

        it("populates artists using the artist service", function () {
            expect(mockArtistService.query).toHaveBeenCalled();
            expect(mockArtistService.query).toHaveBeenCalledWith(3, initialPageSize, "", jasmine.any(Function));
            expect($scope.Artists).toEqual(artists);
        });

        it("populates total count using the artist service", function () {
            expect(mockArtistService.count).toHaveBeenCalled();
            expect(mockArtistService.count).toHaveBeenCalledWith("", jasmine.any(Function));
            expect($scope.Paging.TotalCount).toEqual(77);
        });

    });

    describe("search", function () {
        beforeEach(function () {
            mockArtistService.count.calls.reset();
            mockArtistService.query.calls.reset();
            expect(mockArtistService.count).not.toHaveBeenCalled();
            expect(mockArtistService.query).not.toHaveBeenCalled();

            $scope.Artists = null;
            $scope.Paging.TotalCount = 0;
            expect($scope.Artists).toBeNull();
            expect($scope.Paging.TotalCount).toEqual(0);

            $scope.Paging.PageSize = updatedPageSize;
            $scope.SearchText = "something";
            $scope.Search(true);
        });
        it("resets to first page", function () {
            expect($scope.Paging.CurrentPage).toEqual(0);
        });
        it("queries the artist service for data", function () {
            expect(mockArtistService.query).toHaveBeenCalled();
            expect(mockArtistService.query).toHaveBeenCalledWith(0, updatedPageSize, "something", jasmine.any(Function));
            expect($scope.Artists).toEqual(artists);
        });
        it("queries the artist service for total count", function () {
            expect(mockArtistService.count).toHaveBeenCalled();
            expect(mockArtistService.count).toHaveBeenCalledWith("something", jasmine.any(Function));
            expect($scope.Paging.TotalCount).toEqual(77);
        });
    });

    describe("delete", function () {
        it("calls delete in the artist service", function () {
            $scope.Delete(5);
            expect(mockArtistService.delete).toHaveBeenCalled();
            expect(mockArtistService.delete).toHaveBeenCalledWith(5, jasmine.any(Function));
        });
        it("repopulates artists after the call completes", function () {
            $scope.Artists = null;
            expect($scope.Artists).toBeNull();
            $scope.Delete(5);
            expect($scope.Artists).toEqual(artists);
        });
    });

});