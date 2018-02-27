import './node_modules/bootstrap/dist/css/bootstrap.min.css';
import './node_modules/bootstrap/dist/js/bootstrap.min.js';
import './app/styles/styles.css';
import { QueryInterceptor } from "./app/query-interceptor";
import { RequestController } from "./app/request-controller";
import { ProductService } from "./app/product-service";
import { ProductsView } from "./app/products-view";

(function () {
    new RequestController(
        new QueryInterceptor(),
        new ProductService(),
        new ProductsView()
    );
}());