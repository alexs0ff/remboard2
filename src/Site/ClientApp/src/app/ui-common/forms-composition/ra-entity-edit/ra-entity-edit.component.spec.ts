import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaEntityEditComponent } from './ra-entity-edit.component';

describe('RaEntityEditComponent', () => {
  let component: RaEntityEditComponent;
  let fixture: ComponentFixture<RaEntityEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaEntityEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaEntityEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
