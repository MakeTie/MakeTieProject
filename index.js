import { QueryInterceptor } from "./app/query-interceptor";
import { RequestController } from "./app/request-controller";

(function () {
    new RequestController(
        new QueryInterceptor()
    );
}());