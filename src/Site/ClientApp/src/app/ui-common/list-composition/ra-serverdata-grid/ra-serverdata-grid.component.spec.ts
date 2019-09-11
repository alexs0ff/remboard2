import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaServerdataGridComponent } from './ra-serverdata-grid.component';

describe('RaServerdataGridComponent', () => {
  let component: RaServerdataGridComponent;
  let fixture: ComponentFixture<RaServerdataGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaServerdataGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaServerdataGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
