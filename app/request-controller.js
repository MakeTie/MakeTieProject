export class RequestController {
    constructor(queryInterceptor) {
        this.queryInterceptor = queryInterceptor;

        this.setEventHandlers();
    }

    setEventHandlers() {
        document.addEventListener('DOMContentLoaded', () => {
            this.queryInterceptor.getQuery()
                .then((query) => $("#output").text(query));
        });
    }
}