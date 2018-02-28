export class RequestController {
    constructor(queryInterceptor, productService, productsView) {
        this.queryInterceptor = queryInterceptor;
        this.productService = productService;
        this.productsView = productsView;

        this.setEventHandlers();
    }

    setEventHandlers() {
        document.addEventListener('DOMContentLoaded', () => {
            this.queryInterceptor.getQuery()
                .then((query) => {
                    this.displayQuery(query);
                    this.getAndDisplayProducts(query);
                });
        });
    }

    getAndDisplayProducts(query) {
        return this.productService.getProducts(query)
            .then(products => this.productsView.renderProducts(products));
    }

    displayQuery(query) {
        let output = $("#output");
        output.text(query);
    }
}