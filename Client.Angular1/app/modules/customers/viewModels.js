function CustomerViewModel(customerReadModel) {
        var readModel = customerReadModel;
        this.dataType = 'customer';

        Object.defineProperty(this, 'displayName', {
            get: function () {
                return readModel.name;
            }
        });

        Object.defineProperty(this, 'id', {
            get: function () {
                return readModel.id;
            }
        });
    };