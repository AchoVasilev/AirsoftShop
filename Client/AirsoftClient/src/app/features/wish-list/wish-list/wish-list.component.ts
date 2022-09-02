import { Component, OnInit } from '@angular/core';
import { AddGunToWishListModel } from 'src/app/models/wishList/addGunToWishListModel';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {
  item: AddGunToWishListModel[] = [];

  constructor() { }

  ngOnInit(): void {
  }


}
