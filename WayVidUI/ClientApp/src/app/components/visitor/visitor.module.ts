import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {VisitorMainPageComponent} from './visitor-main-page/visitor-main-page.component';
import {Router, RouterModule} from '@angular/router';


@NgModule({
  declarations: [
    VisitorMainPageComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      {
        path: 'visitor',
        component: VisitorMainPageComponent// ,
        // children: []
      },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class VisitorModule {
}
