// import { Component  } from '@angular/core';
// import {HttpClient, HttpHeaders} from '@angular/common/http';
// import {RequestOptions} from "@angular/http";
//
// @Component({
//   selector: 'app-product',
//   templateUrl: './product.component.html'
// })
// export class ProductComponent {
//   public products: Product[];
//   public url: string;
//   public http: HttpClient;
//
//   constructor(http: HttpClient) {
//     this.http = http;
//     this.url = 'https://192.168.1.12:8080/item'
//     this.http.get<Product[]>(this.url).subscribe(data =>
//       this.products = data , error => console.error(error));
//   }
//
//   public putReq() {
//
//     var data = {
//       "name": "grzyb",
//       "groupName": "spozywcze",
//       "price": 0.99,
//       "availableAmount": 4056
//     }
//     let headers = new HttpHeaders();
//     headers.append("Content-Type", "application/json");
//     this.url = 'https://192.168.1.12:8080/item'
//     this.http.post(this.url, data, {headers: headers}).subscribe();
//     this.http.get<Product[]>(this.url).subscribe(data =>
//       this.products = data , error => console.error(error));
//   }
// }
//
// interface Product {
//   name: string;
//   groupName: string;
//   price: string;
//   availableAmount: string;
// }


import { Component  } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {RequestOptions} from "@angular/http";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  public books: Book[];
  public url: string;
  public http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
    this.url = 'http://192.168.1.70:3002/api/books'
    this.http.get<Book[]>(this.url).subscribe(data => {
      this.books = data;
    }, error => console.error(error));
  }

  public putReq() {

    var data = {
      "Title": "Pan Mickiewicz",
      "Year": 1995,
      "Price": 9.88,
      "Genre": "Pies"
    }
    let headers = new HttpHeaders();
    headers.append("Content-Type", "application/json");
    this.http.post(this.url, data, {headers: headers}).subscribe();
    this.http.get<Book[]>(this.url).subscribe(data =>
      this.books = data , error => console.error(error));
  }
}

interface Book {
  Id: number;
  Title: string;
  Year: number;
  Price: number;
  Genre: string;
}
