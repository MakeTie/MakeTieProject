import Mustache from '../node_modules/mustache/mustache.min.js';
var productsTemplate = require('./templates/products-template.mst');

export class ProductsView {
    constructor() {
        this.productsBlock = $('div[bind=products]')[0];
        this.loader = $("div[bind=loader]");
        this.queryLabel = $("span[bind=querylabel]"); 
        this.errorMessage = $("h1[bind=errormessage]"); 
        this.errorBlock = $(".error-block"); 
        this.alertHeader = $("#header-alert");
        this.uesGoogleBlock = $("div[bind=usegoogleblock]");
    }

    renderProducts(products){
        var that = this;
        this.productsBlock.innerHTML = '';
    
        $.get(productsTemplate, function (template) {
          var rendered = Mustache.render(template, { products });
          that.productsBlock.innerHTML = rendered;
        });
    }

    showLoader(){
        this.loader.show();
    }

    hideLoader(){
        this.loader.css("display", "none");
    }

    displayQuery(query) {
        this.queryLabel.text(query);
    }

    displayErrorMessage(){
        this.hideLoader();
        this.alertHeader.css("display", "none");
        this.queryLabel.css("display", "none");
        this.errorMessage.css("display", "block");
        this.errorBlock.css("display", "block");
        this.errorMessage.text("Service unavaliable!");
    }

    displayGooglePageMessage(){
        this.hideLoader();
        this.alertHeader.css("display", "none");
        this.queryLabel.css("display", "none");
        this.errorBlock.css("display", "none");
        this.uesGoogleBlock.css("display", "block");
    }
}