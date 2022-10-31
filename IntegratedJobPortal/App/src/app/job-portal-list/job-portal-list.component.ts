import { Component, OnInit } from '@angular/core';
import { ApiResponse, CustomStatusCode } from 'src/models/ApiResponse';
import { JobPortal } from 'src/models/Portal';
import { PortalService } from 'src/services/portal.service';

@Component({
  selector: 'app-job-portal-list',
  templateUrl: './job-portal-list.component.html',
  styleUrls: ['./job-portal-list.component.css']
})
export class JobPortalListComponent implements OnInit {
  portalList: JobPortal[]
  constructor(private httpService: PortalService) { }

  ngOnInit(): void {
  }

  loadPortals() {
    this.httpService.getJobPortalsList().subscribe((response: ApiResponse) => {
      if (response.StatusCode == CustomStatusCode.Success) {
        this.portalList = response.Data
        console.log(this.portalList)
      }
    });
  }

}
