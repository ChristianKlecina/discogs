import { Component, OnInit } from '@angular/core';
import {BasketService} from "./basket.service";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  totalPrice !:number;
  public tracks : any []
  constructor(private cartService: BasketService) { }

  ngOnInit(): void {
    this.cartService.getTracksCart().subscribe(response => {
      this.totalPrice = this.cartService.getTotalPrice()
      this.tracks = response
    })
  }

  removeItem(track: any){
    this.cartService.removeCartITem(track);
  }
}
