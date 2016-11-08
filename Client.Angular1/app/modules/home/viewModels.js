function HomeShowcase(showcaseModel) {
        var readModel = showcaseModel;

        Object.defineProperty(this, 'dataType', {
            get: function () {
                return 'home-showcase';
            }
        });
    };