import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CuisinesPage } from './cuisines.page';

describe('CuisinesPage', () => {
  let component: CuisinesPage;
  let fixture: ComponentFixture<CuisinesPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CuisinesPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CuisinesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
