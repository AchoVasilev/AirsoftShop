import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GunListComponent } from './gun-list.component';

describe('GunListComponent', () => {
  let component: GunListComponent;
  let fixture: ComponentFixture<GunListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GunListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GunListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
