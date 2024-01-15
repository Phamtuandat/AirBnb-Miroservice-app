import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PropertyService } from '../property.service';
import { Property } from '../../Shared/Models';
import { AppToolbarComponent } from '../../Shared/components/header/app-toolbar.component';

@Component({
  selector: 'app-property-detail',
  standalone: true,
  imports: [RouterModule, AppToolbarComponent],
  templateUrl: './property-detail.component.html',
  styleUrl: './property-detail.component.scss',
})
export class PropertyDetailComponent implements OnInit {
  constructor(public router: ActivatedRoute, private _api: PropertyService) {}
  property: Property | null = null;
  ngOnInit(): void {
    this.router.params.subscribe((params) => {
      const id = params['id'];
      if (id) {
        this._api.getProperty(id).subscribe((p) => (this.property = p));
      }
    });
  }
}
