import { Component, OnInit } from '@angular/core';
import { DealerGunsList } from 'src/app/models/products/guns/dealerGunList';
import { GunService } from 'src/app/services/products/guns/gun.service';

@Component({
  selector: 'app-mine',
  templateUrl: './mine.component.html',
  styleUrls: ['./mine.component.css']
})
export class MineComponent implements OnInit {
  guns: DealerGunsList[] = [];
  isLoading: boolean = true;
  isLoaded: boolean = false;

  constructor(private gunService: GunService) { }

  ngOnInit(): void {
    this.loadGuns();
    this.isLoading = false;
    this.isLoaded = true;
  }

  loadGuns(): void {
    this.gunService.getDealerProducts()
      .subscribe(res => this.guns = res);
  }
}
