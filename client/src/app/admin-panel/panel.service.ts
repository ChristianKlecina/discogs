import { Injectable } from '@angular/core';
import {IGenre} from "../shared/models/genre";
import {IMedium} from "../shared/models/medium";
import {Cart} from "../shared/models/cart";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PanelService {

  token = localStorage.getItem("jwt")



  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: `Bearer ${this.token}`
    })
  };

  constructor(private httpClient: HttpClient) {

  }

  getGenres() {
    return this.httpClient.get<IGenre[]>('https://localhost:1296/api/track/genre')
  }
  getMediums() {
    return this.httpClient.get<IMedium[]>('https://localhost:1296/api/track/medium')
  }

  getCarts() {
    return this.httpClient.get<Cart[]>('https://localhost:1296/api/cart')
  }

  insertGenre(genreName: string){
    console.log(genreName)
    this.httpClient.post('https://localhost:1296/api/track/genre', {'genreName' : genreName}, this.httpOptions ).subscribe(response => {
      console.log(response)
    }, error => {
      console.log(error)
    })
    location.reload()
  }

  deleteGenre(id) {
    this.httpClient.delete('https://localhost:1296/api/track/genre/'+id,this.httpOptions).subscribe(res => {
      console.log(res)
    }, error => {
      console.log(error)
    })
    location.reload()
  }

}
