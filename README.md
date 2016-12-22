# Shopper, a Services UI Composition and SOA sample

The goal of this set of samples is to show concepts expressed in ["The secret of better UI composition"](http://particular.net/blog/secret-of-better-ui-composition) blog post.

## Services

`CustomerCare`, `Marketing`, `Sales`, `Shipping`, and `Warehouse` are Services that compose this samples. The sample mimics a very small subset of an e-commerce system.

## Front-ends

### MvcCoreFrontend

[`MvcCoreFrontend`](https://github.com/mauroservienti/Services-UI-Composition/tree/master/MvcCoreFrontend) is a .Net MVC Core application.

### Client.Angular1

[`Client.Angular1`](https://github.com/mauroservienti/Services-UI-Composition/tree/master/Client.Angular1) is an AngularJS (1.x) single page application.

## Getting started

### API Projects

Configure `Visual Studio` to run the following projects as startup projects:

* `CustomerCare.API.Host`
* `Marketing.API.Host`
* `Sales.API.Host`
* `Shipping.API.Host`
* `Warehouse.API.Host`

The above projects will expose, self-hosting Owin, APIs through data, owned by services, are accessible.

### Single page application

The [single page application](https://github.com/mauroservienti/Services-UI-Composition/tree/master/Client.Angular1) requires `Node.js`, `bower` and `grunt` to be built and served locally:

* Install `Node.js` if not already installed
* Install `bower` globally running at a `Node` command prompt `npm install -g bower`
* Install `grunt` globally running at a `Node` command prompt `npm install -g grunt`
* Install `grunt-cli` globally running at a `Node` command prompt `npm install -g grunt-cli`

Open a `Node` command prompt, move to the [single page application](https://github.com/mauroservienti/Services-UI-Composition/tree/master/Client.Angular1):
* _first time only_: run `npm install` to install all required dependencies
* run `grunt build` to build the single page application
* run `gunt connect` to serve it using the grung default web server (application will be available at `http://localhost:9000`)

