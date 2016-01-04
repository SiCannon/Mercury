describe("ArtistService", function () {

    var artists = [
        { ArtistId: 1, Name: "One" },
        { ArtistId: 2, Name: "Two" },
        { ArtistId: 3, Name: "Three" }
    ];

    var $httpBackend, artistService;

    beforeEach(module("app"));

    beforeEach(inject(function (_$httpBackend_, ArtistService) {
        $httpBackend = _$httpBackend_;
        artistService = ArtistService;
    }));

    it("query() should GET XHR and assign result", function () {
        var result;
        $httpBackend.expectGET("/api/Artist?pageNumber=0&pageSize=20").respond(200, artists);
        artistService.query(0, 20, null, function (data) { result = data; });
        $httpBackend.flush();
        expect(result).toEqual(artists);
    });

    it("count() should GET XHR and assign result", function () {
        var result;
        $httpBackend.expectGET("/api/ArtistCount").respond(200, 3);
        artistService.count(null, function (data) { result = data; });
        $httpBackend.flush();
        expect(result).toEqual(3);
    });

    it("get() should GET XHR and assign result", function () {
        var result;
        $httpBackend.expectGET("/api/Artist/3").respond(200, artists[0]);
        artistService.get(3, function (data) { result = data; });
        $httpBackend.flush();
        expect(result).toEqual(artists[0]);
    });

    it("save() should POST XHR and assign result", function () {
        var returnedId;
        $httpBackend.expectPOST("/api/Artist").respond(200, 77);
        artistService.save(artists[0], function (x) { returnedId = x; });
        $httpBackend.flush();
        expect(returnedId).toEqual(77);
    });

    it("delete() should DELETE XHR and call success function", function () {
        var callback = jasmine.createSpy();
        $httpBackend.expectDELETE("/api/Artist/77").respond(200);
        artistService.delete(77, callback);
        $httpBackend.flush();
        expect(callback).toHaveBeenCalled();
    });


});