import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryViewModel } from 'src/app/models/categories/categoryViewModel';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CategoryService } from 'src/app/services/categories/category.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  categories: CategoryViewModel[] = [];

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }

  get isClient(): boolean {
    return this.authService.getClient();
  }

  constructor(
    private authService: AuthService,
    private categoryService: CategoryService,
    private router: Router) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.loadCategories()
      .subscribe(res => this.categories = res);
  }

  catalogClick(): void {
    let catalog = document.getElementById('nav-catalog')!;
    const icon = document.querySelector('#catalog-btn > i')!;

    if (catalog.style.display == '' || catalog.style.display == 'none') {
      catalog.style.display = 'block';
    } else {
      catalog.style.display = 'none';
    }

    if (icon.classList.contains('fa-bars')) {
      icon.classList.remove('fa-bars');
      icon.classList.add('fa-xmark');
    } else if (icon.classList.contains('fa-xmark')) {
      icon.classList.remove('fa-xmark');
      icon.classList.add('fa-bars');
    }
  }

  mouseOut(): void {
    let catalog = document.getElementById('nav-catalog')!;
    const icon = document.querySelector('#catalog-btn > i')!;
    catalog.style.display = 'none';

    if (icon.classList.contains('fa-xmark')) {
      icon.classList.remove('fa-xmark');
      icon.classList.add('fa-bars');
    }
  }

  logOut(): void {
    this.authService.logOut();
    this.router.navigate(['/']);
  }
}
