import Mustache from '../node_modules/mustache/mustache.min.js';
var productsTemplate = require('./templates/products-template.mst');

export class ProductsView {
    constructor() {
        this.productsBlock = $('div[bind=products]')[0];
    }

    renderProducts(products){
        var that = this;
        this.productsBlock.innerHTML = '';
    
        $.get(productsTemplate, function (template) {
          var rendered = Mustache.render(template, { products });
          that.productsBlock.innerHTML = rendered;
        });
    }
}