# AmericaVirtualChallenge
API Service and Web Project for America Virtual recruitment proccess
nowledgements section. Here are a few examples.

<!--
*** Thanks for checking out this README Template. If you have a suggestion that would
*** make this better, please fork the repo and create a pull request or simply open
*** an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
-->





<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<p align="center">
  <h3 align="center">README</h3>
</p>



<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Roadmap](#roadmap)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project

This is the challenge for Virtual America recruiment process. It is a site to manage users, products and orders.
You can watch the online version on: [https://americavirtualchallenge.azurewebsites.net/](https://americavirtualchallenge.azurewebsites.net/)

It contains:
* Products module to manage them (CRUD)
* User and roles managment, fuctionalities by role and user.
* Pagination of entities.
* API service to get a token authentication.
* API service to comsume the methods GET // POST // PUT // DELETE  for Product Entity.
* Log entity to trace some system activity

### Built With
* [.Net Core](https://dotnet.microsoft.com/download)
* [Bootstrap](https://getbootstrap.com)
* [JQuery](https://jquery.com)


<!-- GETTING STARTED -->
## Getting Started

There are the basic instructions on setting up for installing and running the project.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* visual studio
* dotnet

### Installation

1. Clone the repo 
```sh
https://github.com/sevannr/AmericaVirtualChallengue.git
```
3. Run it

### Installation
Loggin:
* The project include 3 default users:
  * sevann.radhak@gmail.com -> roles: admin, user
  * virtual.america@gmail.com -> roles: user
  * homero.simpson@gmail.com -> roles: user
 
 Roles:
 * Admin role can use Product CRUD
 * Admin role can use User CRUD
 * Admin role can see all orders
 * Admin role can see all order details
 * User role can update own information, password too!
 * User role can add products to orders
 * User role can register order
 * User role can see the own orders
 * User role can see the own order details
 
 After loggin as a User, you can register products to orders, modify it and send the order.
 After loggin as a Admin, you can use CRUD for products and users, and see all orders
  
<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Sevann Radhak T - sevann.radhak@gmail.com

Project Link: [https://github.com/sevannr/AmericaVirtualChallengue.git](https://github.com/sevannr/AmericaVirtualChallengue.git)
