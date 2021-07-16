Catalog = {
    _properties: {
        getViewLink: ""
    },

    init: properties => {
        $.extend(Catalog._properties, properties);

        $(".pagination li a").click(Catalog.clickOnPage);
    },

    clickOnPage: function(e) {
        e.preventDefault();

        const button = $(this);


    }
}