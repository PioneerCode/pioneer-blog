import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'pc-loader',
  templateUrl: './app/components/loader/loader.component.html'
})

export class LoaderComponent implements OnInit {
  loading = true;

  ngOnInit(): void {

  }
}