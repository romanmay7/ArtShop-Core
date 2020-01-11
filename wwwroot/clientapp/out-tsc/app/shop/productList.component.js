import * as tslib_1 from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor(data) {
        this.data = data;
        this.products = [];
        this.products = data.products;
    }
    ngOnInit() {
        this.data.loadProducts()
            .subscribe(success => {
            if (success) {
                this.products = this.data.products;
            }
        });
    }
    addProduct(product) {
        this.data.AddToOrder(product);
    }
};
ProductList = tslib_1.__decorate([
    Component({
        selector: "product-list",
        templateUrl: "productList.component.html",
        styleUrls: ["productList.component.css"]
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=productList.component.js.map