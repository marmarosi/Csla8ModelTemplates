import{_ as b,l as y,ɵ as f,a as h,b as c,m as O,d as g,g as s,n as k,i as T,o as w,j as $,p as L,q as F,f as D,r as I,c as x,s as d,h as v,t as M,k as P,e as u,R}from"./index-CH4RD9oS.js";function S(n,a,t){return n.set(b(n,a),t),t}const E=(n,a)=>a.id;function j(n,a){if(n&1&&(c(0,"li")(1,"a",0)(2,"div",1),x(3),g()()()),n&2){const t=d().$implicit,r=d();s(),v("routerLink",t.slug),s(),M("padding-left",r.getPaddingLeft(t)),s(),P(" ",t.title," ")}}function z(n,a){if(n&1&&(c(0,"li")(1,"a",0),u(2,"img",2),c(3,"div",1),x(4),g()()()),n&2){const t=d().$implicit,r=d();s(),v("routerLink",t.slug),s(2),M("padding-left",r.getPaddingLeft(t)),s(),P(" ",t.title," ")}}function B(n,a){if(n&1&&D(0,j,4,4,"li")(1,z,5,4,"li"),n&2){const t=a.$implicit;I(t.children.length===0?0:1)}}let N=(()=>{var n,a=new WeakMap;class t{constructor(o){w(this,a,void 0),this.router=o,S(a,this,"/src/content/templates/"),this.tree=[],this.contents=$(e=>e.filename.startsWith(L(a,this))).map(e=>{const l=e.attributes.slug.lastIndexOf("."),_=l<0?"":`${e.attributes.slug.substring(0,l)}`;return{id:"",slug:e.attributes.slug,title:e.attributes.title,branch:_,position:e.attributes.position,level:0,children:[]}}),this.setNodes("",0,"",this.tree),this.contents.length=0,this.flatTree(this.tree),this.tree.length=0}setNodes(o,e,l,_){this.contents.filter(i=>(i.branch??"")===l).sort(function(i,p){return i.position-p.position}).forEach((i,p)=>{i.id=o?`${o}.${p+1}`:(p+1).toString(),i.level=e;const m=i.slug.lastIndexOf("."),C=m<0?l?`${l}.${i.slug}`:i.slug:`${l}.${i.slug.substring(m+1)}`;this.setNodes(i.id,e+1,C,i.children),_.push(i)})}flatTree(o){o.forEach(e=>{this.contents.push(e),this.flatTree(e.children)})}getPaddingLeft(o){return`${o.level*10}px`}getLink(o){return o.split("/")}}return n=t,n.ɵfac=function(o){return new(o||n)(y(F))},n.ɵcmp=f({type:n,selectors:[["app-template-menu"]],standalone:!0,features:[h],decls:3,vars:0,consts:[[3,"routerLink"],[1,"link-text"],["src","chevron-down.svg","alt","",1,"chevron"]],template:function(o,e){o&1&&(c(0,"ul"),O(1,B,2,1,null,null,E),g()),o&2&&(s(),k(e.contents))},dependencies:[T],styles:[`[_ngcontent-%COMP%]:root {
  --font-family: Roboto, sans-serif;
  --font-size: 16px;
  --line-height: 24px;
  --font-weight: 400;
  --main-bg-color: white;
  --main-text-color: #666;
  --main-link-color: cornflowerblue;
  --main-link-shadow: #e8effc;
  --primary-bg-color: #800020;
  --primary-bg-color-lighter: #9b0027;
  --primary-bg-color-darker: #77001e;
  --primary-text-color: white;
  --primary-link-color: gold;
  --secondary-bg-color: #F2E8C6;
  --secondary-text-color: #800020;
  --secondary-link-color: #b9002e;
  --footer-bg-color: #DAD4B5;
  --footer-text-color: black;
}

html[_ngcontent-%COMP%], 
body[_ngcontent-%COMP%] {
  height: 100%;
  font-family: var(--font-family);
  font-size: var(--font-size);
  line-height: var(--line-height);
  font-weight: var(--font-weight);
  background-color: var(--footer-bg-color);
  color: var(--main-text-color);
  margin: 0;
  padding: 0;
}

.fill-remaining-space[_ngcontent-%COMP%] {
  flex: 1 1 auto;
  display: inline-block;
}

a[_ngcontent-%COMP%], a[_ngcontent-%COMP%]:visited {
  color: var(--main-link-color);
}

ul[_ngcontent-%COMP%] {
  margin: 0;
  padding: 0;
  list-style: none;
}

.text--left[_ngcontent-%COMP%] {
  text-align: left;
}

.text-center[_ngcontent-%COMP%] {
  text-align: center;
}

.text-right[_ngcontent-%COMP%] {
  text-align: right;
}

table[_ngcontent-%COMP%] {
  border-collapse: collapse;
}

th[_ngcontent-%COMP%] {
  background-color: var(--secondary-bg-color);
  color: var(--secondary-text-color);
  font-weight: normal;
}

td[_ngcontent-%COMP%], th[_ngcontent-%COMP%] {
  border: 1px solid #CCC;
  padding: 0 10px;
}







































































































































li[_ngcontent-%COMP%] {
  width: 240px;
}

a[_ngcontent-%COMP%] {
  text-decoration: none;
  display: block;
  padding: 0 10px;
  line-height: 32px;
}

.chevron[_ngcontent-%COMP%] {
  float: right;
  height: 16px;
  margin-top: 8px;
}

a[_ngcontent-%COMP%]:hover, a[_ngcontent-%COMP%]:active {
  background-color: var(--main-link-shadow);
}

.link-text[_ngcontent-%COMP%] {
  display: inline-block;
}`]}),t})(),q=(()=>{var n;class a{}return n=a,n.ɵfac=function(r){return new(r||n)},n.ɵcmp=f({type:n,selectors:[["ng-component"]],standalone:!0,features:[h],decls:4,vars:0,consts:[[1,"template-menu"],[1,"fill-remaining-space"]],template:function(r,o){r&1&&(c(0,"aside",0),u(1,"app-template-menu"),g(),c(2,"main",1),u(3,"router-outlet"),g())},dependencies:[R,N],styles:[`[_nghost-%COMP%] {
  display: flex;
}

aside[_ngcontent-%COMP%] {
  padding: 20px 10px;
}

main[_ngcontent-%COMP%] {
  padding: 0 10px 20px 0;
  max-width: 800px;
}`]}),a})();export{q as default};
