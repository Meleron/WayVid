import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VisitorMainPageComponent } from './visitor-main-page.component';

describe('VisitorMainPageComponent', () => {
  let component: VisitorMainPageComponent;
  let fixture: ComponentFixture<VisitorMainPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VisitorMainPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VisitorMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
