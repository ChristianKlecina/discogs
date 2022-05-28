import {Injectable, Output} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {ITrack} from "../shared/models/track";

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  public cartItemList : any = []

  public trackList = new BehaviorSubject<any>([]);
  constructor() { }

  getTracksCart(){
    return this.trackList.asObservable();
  }

  setTrackCart(track: any){
    this.cartItemList.push(...track);
    this.trackList.next(track);
  }

  addToCart(track: any){
    this.cartItemList.push(track)
    this.trackList.next(this.cartItemList)
    this.getTotalPrice();
    console.log(this.cartItemList)
  }

  getTotalPrice(): number{
    let grandTotal = 0;
    this.cartItemList.map((a:any) => {
      grandTotal += a.price;
    })
    return grandTotal;
  }

  removeCartITem(track : any){
    this.cartItemList.map((a:any, index:any) => {
      if(track.id === a.id){
        this.cartItemList.splice(index,1)
      }
    })
    //console.log(this.cartItemList)
    this.trackList.next(this.cartItemList)
  }

  removeAllCart(){
    this.cartItemList = []
    this.trackList.next(this.cartItemList)
  }


}
