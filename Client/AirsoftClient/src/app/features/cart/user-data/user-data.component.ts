import { Component, OnInit } from '@angular/core';
import { UserClientViewModel } from 'src/app/models/client/userClientViewModel';
import { ClientService } from 'src/app/services/client/client.service';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-user-data',
  templateUrl: './user-data.component.html',
  styleUrls: ['./user-data.component.css']
})
export class UserDataComponent implements OnInit {
  client!: UserClientViewModel;
  isLoaded: boolean = false;
  isLoading: boolean = true;

  constructor(private dataService: DataService, private clientService: ClientService) { }

  ngOnInit(): void {
    this.getUserData();
    this.isLoaded = true;
    this.isLoading = false;
  }

  getUserData(): void {
    this.clientService.getClientData()
      .subscribe(res => {
        this.client = res;
        this.setClientData(this.client);
      });
  }

  setClientData(value: UserClientViewModel) {
    this.dataService.changeUserData(value);
  }
}
