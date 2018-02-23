//import { SERVER_URL } from "./global-variables";

export class ProductService {
  constructor(){
    this.REQUEST_PATH = "api/recommendation"
  }

  getProducts(query) {
    var url = "http://localhost:61809/" + this.REQUEST_PATH + '?query=' + query;

    var promise = new Promise(function (resolve, reject) {
      $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
          //var products = JSON.parse(response);
          resolve(response);
        },
        error: function () {
          reject("Getting data error.");
        }
      });
    });
    
    return promise;
  }
}