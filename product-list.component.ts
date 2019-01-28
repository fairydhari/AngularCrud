import { Component, OnInit } from '@angular/core';
//import { process, State } from '@progress/kendo-data-query';
import { products } from '../services/Product'
import { DataStateChangeEvent, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { State,process,SortDescriptor, orderBy,filterBy} from '@progress/kendo-data-query';
import { filter } from '@progress/kendo-data-query/dist/npm/transducers';
import { RouterModule, Routes, Router } from '@angular/router';
//import { SortDescriptor, orderBy } from '@progress/kendo-data-query';



@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',

  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  //private filter: string;
  private sort: SortDescriptor[] = [];
  private gridView: GridDataResult;
  public products: any[]=products;;
  prods:any='';
  public cproducts: any[];
  searchText: string;
  showAllData:boolean=false;
  public buttonCount = 5;
  public info = true;
  public type: 'numeric' | 'input' = 'numeric';
  public pageSizes = true;
  public previousNext = true;
  public pageSize = 5;
  public skip = 0;
numberOfRecords:number;
  constructor(private router: Router) {
   
   }
   protected sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
    this.loadProducts(this.prods);
}

/*private products: any[] = Array(100).fill({}).map((x, idx) => ({
    'ProductID': idx,
    'ProductName': 'Product' + idx,
    'Discontinued': idx % 2 === 0
}));*/
/* public filterData = {
          logic: 'or',
          filters: [{ field: 'ProductName', operator: 'contains', value: 'C' }
          ]
 };*/
 public filterData = {
 // logic: 'or',
  filters: [{ field: 'ProductName', operator: 'contains', value: 'Chef' }, 
  { field: 'ProductName', operator: 'contains', value: 'Chang' }]
};

private loadProducts(prods): void {
  console.log("prods"+this.prods)
  //const products = prods || this.products;
  if(prods=='')
  {
    console.log("prods condition"+this.prods);
    if(this.showAllData==true)
    {
      this.pageSize=10;
    }
    else
    {
     //this.products = products.slice(0,5);
     this.pageSize=5;
    }
    console.log(this.pageSize);
    this.products=products.slice(this.skip, this.skip + this.pageSize);
    this.numberOfRecords=products.length;
   /* this.gridView = {
      data: orderBy(this.products, this.sort),
      total: products.length,
      
  };*/
   /* items.filter( it => {
      return it.toLowerCase().includes(searchText);
    });*/
    /*this.searchText=prods;
   /* this.cproducts.filter(this.searchText); //products.filter(product => product.ProductName.toLowerCase().includes(this.prods)); 
    console.log(this.cproducts); */
  } 
  else
  {
   // console.log("else"+this.prods);
   this.pageSize=10;
   var searchresult=products.filter(product => product.ProductName.toLowerCase().includes(prods))
    this.products = searchresult.slice(this.skip, this.skip + this.pageSize);
    this.numberOfRecords=searchresult.length;
  }
  
  this.gridView = {
    data: orderBy(this.products, this.sort),
    total:this.numberOfRecords
    
};
   
}
protected pageChange({ skip, take }: PageChangeEvent): void {
  this.skip = skip;
  this.pageSize = take;
  this.loadProducts(this.prods);
}
private change(event:any){
  console.log(event.target.value);
  //if(this.filter!='')
 // {
    //console.log("not null"+this.filter);
    //this.loadProducts(this.filter);
 //   var s=products.filter(product => product.ProductName.toLowerCase().includes(event.target.value));
  //  console.log(s);
   /* var s1=products.filter({
      logic: 'or',
      filters: [{ field: 'ProductName', operator: 'contains', value: 'C' }
      ]
});*/
   // console.log(s1);
   this.prods=event.target.value;
    this.loadProducts(event.target.value);
    //this.loadProducts(products.filter(product => product.ProductName.toLowerCase().includes(event.target.value)));
 /* }
  else
  {
    console.log("empty"+this.filter);
    this.loadProducts(this.prods);
  }*/
}
/*  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.gridData = process(sampleProducts, this.state);
}*/
  
  
 /* public state: State = {
    skip: 0,
    take: 5,

    // Initial filter descriptor
  /*  filter: {
      logic: 'and',
      filters: [{ field: 'ProductName', operator: 'contains', value: 'Chef' }]
    }*/
/*};
public gridData: GridDataResult = process(sampleProducts, this.state);*/
  ngOnInit() {
    this.loadProducts(this.prods);
  }
  showall()
  {
     this.showAllData=true;
     this.loadProducts(this.prods);
  }
  nextPage()
  {
    this.router.navigate(['/userDetails']);
  }
}
