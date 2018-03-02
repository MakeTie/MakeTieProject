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
                    this.productsView.displayQuery(query);
                    this.productsView.showLoader();
                    this.getAndDisplayProducts(query);
                });
        });
    }

    getAndDisplayProducts(query) {
        return this.productService.getProducts(query)
            .then(products => {
                this.productsView.hideLoader();
                this.productsView.renderProducts(products);
            });
    }
}