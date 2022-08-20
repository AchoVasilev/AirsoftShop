import { Component, OnInit } from '@angular/core';
import { UserDealerViewModel } from 'src/app/models/dealers/userDealerViewModel';
import { DealerService } from 'src/app/services/dealer/dealer.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  dealer: UserDealerViewModel | undefined;
  isLoaded: boolean = false;
  isLoading: boolean = true;

  constructor(private dealerService: DealerService) { }

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile() {
    this.dealerService.getDealerData()
      .subscribe({
        next: (res: UserDealerViewModel) => {
          this.dealer = res;
          setTimeout(() => {
            this.isLoaded = true;
            this.isLoading = false;
          }, 700);
        }
      });
  }
}
