import { HttpClient,HttpHeaders } from "@angular/common/http";
//import { Http, Response, Headers } from "@angular/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { Product } from "./product";
//import { Order, OrderItem } from "./order";
import * as OrderNS from  "./order";

@Injectable()
export class DataService
{

    constructor(private http: HttpClient) { }
    public order: OrderNS.Order = new OrderNS.Order();
    public products: Product[] = [];


    private token: string = "";
    private tokenExpiration: Date;
    //private currentUser: string;

    //loadProducts(): Observable<Product[]> {
    //    return this.http.get("/api/products")
    //    map((result: Response) => this.products = result.json());

    //}

    loadProducts(): Observable<boolean> {
        return this.http.get("/api/products")
            .pipe(
                map((data: any[])=> {
                    this.products = data;
                    return true;
                })

            );
    }

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }

    login(creds): Observable<boolean> {
        return this.http
            .post("/Account/CreateToken", creds)
            .pipe(
                map((data: any) => {
                    this.token = data.token;
                    this.tokenExpiration = data.expiration;
                    //this.currentUser = creds.username;
                    return true;
                })
                
            );
    }

     //checkout needs to post to the api with the token information
     //that we just received after login.import Header and use it modify header
     //information during the posting of orders to the api. 
   
//    public checkout() {
//    //order number is required on the API
//    if (!this.order.orderNumber) {
//        this.order.orderNumber = this.order.orderDate.getFullYear().toString()
//            + this.order.orderDate.getTime().toString();
//    }

//        return this.http.post("/api/orders", this.order, {
//            headers: new Headers({
//                "Authorization": "Bearer " + this.token
//            })
//        })
//            .pipe(
//                map(response => {
//                    this.order = new OrderNS.Order();
//                    return true;
//                })
//            );
//}
    public checkout() {
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
            
        }
        //this.order.User = this.currentUser;

        return this.http.post("api/Orders", this.order, {
            headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
                                      .set('Content-Type', 'application/json; charset=utf-8')
        })
            .pipe(
                map(response => {
                    this.order = new OrderNS.Order();
                    return true;
                })

            );
    }

    public AddToOrder(product: Product) {
    //    if (this.order) {
    //        this.order = new OrderNS.Order();
    //    }

        let item: OrderNS.OrderItem = this.order.items.find(i => i.productId == product.id);

         if (item) { item.quantity++;}

         else {
             item = new OrderNS.OrderItem();
             item.productId = product.id;
             item.productArtist = product.artist;
             item.productArtId = product.artId;
             item.productCategory = product.category;
             item.productSize = product.size;
             item.productTitle = product.title;
             item.unitPrice = product.price;
             item.quantity = 1;

             this.order.items.push(item);
         }

    }

    //public products = [
    //    {
    //        title: "First Product",
    //        price: 19.99
    //    },
    //    {
    //        title: "Second Product",
    //        price: 9.99
    //    },
    //    {
    //        title: "Third Product",
    //        price: 14.99
    //    }

    //]

}