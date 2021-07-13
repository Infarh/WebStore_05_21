Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: "",
        decrementLink: "",
        removeFromCartLink: ""
    },

    init: function(properties) {
        $.extend(Cart._properties, properties);

        Cart.initEvents();
    },

    initEvents: function() {
        $(".add-to-cart").click(Cart.addToCart);

        $(".cart_quantity_up").click(Cart.incrementItem);
        $(".cart_quantity_down").click(Cart.decrementItem);
        $(".cart_quantity_delete").click(Cart.removeItem);
    },

    addToCart: function(event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function() {
                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function() { console.log("addToCart fail"); });
    },

    showToolTip: function(button) {
        button.tooltip({ title: "Добавлено в корзину!" }).tooltip("show");
        setTimeout(function() {
            button.tooltip("destroy");
        }, 500);
    },

    refreshCartView: function() {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function(cartHtml) {
                container.html(cartHtml);
            })
            .fail(function () { console.log("refreshCartView fail"); });
    },

    incrementItem: function(event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        var tr = button.closest("tr");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function() {
                const count = parseInt($(".cart_quantity_input", tr).val());
                $(".cart_quantity_input", tr).val(count + 1);
                Cart.refreshPrice(tr);
                Cart.refreshCartView();

            })
            .fail(function() { console.log("incrementItem fail") });
    },

    refreshPrice: function(tr) {
        const count = parseInt($(".cart_quantity_input", tr).val());
        const price = parseInt($(".cart_price", tr).data("price"));

        const totalPrice = price * count;

        const value = totalPrice.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });

        const cartTotalPrice = $(".cart_total_price", tr);
        cartTotalPrice.data("price", totalPrice);
        cartTotalPrice.html(value);

        Cart.refreshTotalPrice();
    },

    refreshTotalPrice: function() {
        var totalPrice = 0;

        $(".cart_total_price").each(function() {
            const price = parseFloat($(this).data("price"));
            totalPrice += price;
        });

        const value = totalPrice.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });

        $("#total-order-price").html(value);
    },

    decrementItem: function(event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        var tr = button.closest("tr");

        $.get(Cart._properties.decrementLink + "/" + id)
            .done(function() {
                const count = parseInt($(".cart_quantity_input", tr).val());
                if (count > 1) {
                    $(".cart_quantity_input", tr).val(count - 1);
                    Cart.refreshPrice(tr);
                } else {
                    tr.remove();
                    Cart.refreshTotalPrice();
                }

                Cart.refreshCartView();
            })
            .fail(function () { console.log("decrementItem fail") });
    },

    removeItem: function(event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.removeFromCartLink + "/" + id)
            .done(function() {
                button.closest("tr").remove();
                Cart.refreshTotalPrice();
                Cart.refreshCartView();
            })
            .fail(function () { console.log("removeItem fail") });
    }
}