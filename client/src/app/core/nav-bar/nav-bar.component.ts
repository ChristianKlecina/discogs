import {Component, Input, OnInit} from '@angular/core';
import {BasketService} from "../../basket/basket.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {


  totalItem = 0;
  constructor(private cartService: BasketService) { }

  ngOnInit(): void {
    this.cartService.getTracksCart().subscribe(response => {
      this.totalItem = response.length;
    })

  }

}
