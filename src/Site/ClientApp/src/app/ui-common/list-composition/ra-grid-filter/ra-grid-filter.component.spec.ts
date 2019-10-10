import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaGridFilterComponent } from './ra-grid-filter.component';

describe('RaGridFilterComponent', () => {
  let component: RaGridFilterComponent;
  let fixture: ComponentFixture<RaGridFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaGridFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaGridFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
