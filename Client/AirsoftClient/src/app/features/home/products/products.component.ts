import { Component, OnInit } from '@angular/core';
import { InitialGunViewModel } from 'src/app/models/products/guns/initialGunViewModel';
import { GunService } from 'src/app/services/products/guns/gun.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  isLoaded: boolean = false;
  isLoading: boolean = true;
  guns: InitialGunViewModel[] = [];

  constructor(private gunService: GunService) { }

  ngOnInit(): void {
    this.getNewestGuns();
    this.isLoaded = true;
    this.isLoading = false;
  }

  getNewestGuns(): void {
    this.gunService.getNewestEightGuns()
      .subscribe({
        next: (res: InitialGunViewModel[]) => {
          this.guns = res;
          setTimeout(() => {
            this.isLoaded = true;
            this.isLoading = false;
          }, 500);
        },
        error: (err) => {
          this.isLoaded = true;
          this.isLoading = false;
        }
      });
  }
}
