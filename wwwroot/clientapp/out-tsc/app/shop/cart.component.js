import * as tslib_1 from "tslib";
import { Component } from "@angular/core";
let Cart = class Cart {
    constructor(data, router) {
        this.data = data;
        this.router = router;
    }
    onCheckout() {
        if (this.data.loginRequired) {
            //Force Login
            this.router.navigate(["login"]);
        }
        else {
            //Go to checkout
            this.router.navigate(["checkout"]);
        }
    }
};
Cart = tslib_1.__decorate([
    Component({
        selector: "the-cart",
        templateUrl: "cart.component.html",
        styleUrls: []
    })
], Cart);
export { Cart };
//# sourceMappingURL=cart.component.js.map