angular.module("app")
    .factory("PagingInfo", function () {
        return function (initialPageSize) {

            this.getData = function () { }
            this.PageSize = initialPageSize || 20;
            this.CurrentPage = 0;
            this.TotalCount = 0;
            this.MinPage = function () { return 0; }
            this.MaxPage = function () { return Math.ceil(this.TotalCount / this.PageSize) - 1; };

            this.PrevPage = function () {
                if (this.CurrentPage > this.MinPage()) {
                    this.CurrentPage--;
                    this.getData();
                }
            };

            this.NextPage = function () {
                if (this.CurrentPage < this.MaxPage()) {
                    this.CurrentPage++;
                    this.getData();
                }
            };

            this.FirstPage = function () {
                this.CurrentPage = this.MinPage();
                this.getData();
            };

            this.LastPage = function () {
                this.CurrentPage = this.MaxPage();
                this.getData();
            };

        };
    });