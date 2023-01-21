# La pasta e-commerce

<p align="center">
  <img width="253" height="160" src="./docs/farfalle_logo.png" alt="Farfalle pasta logo">
</p>

This project aims to build a tiny e-commerce which sells pasta. The reasons why I'm doing this are:
- I like Pasta
- I'm always hungry
- I'm looking for a sample project to exercise all the new .NET features

Jokes aside, it'd be fantastic to have a very simple an clear project that allows me to apply all the new (and not so new) .NET concepts.

## Run locally using Docker

Prerequisites:
- [docker](https://docs.docker.com/get-docker/)
- [docker-compose](https://docs.docker.com/compose/install/)

From the root folder, start all the containers by running:
```shell
$ docker-compose up
```

The frontend should be accessible at http://localhost:5147, while the backend at http://localhost:5008/.

To remove containers, images and the new network, just run:
```shell
$ docker-compose down --rmi all
```

## Architecture and functionalities

<p align="center">
    <img src="./docs/la-pasta.drawio.svg" alt="Architecture diagram">
</p>

Edit the architecture diagram [here](https://app.diagrams.net/?title=la-pasta.drawio#R1Vddc5swEPw1fmwHEGD70fFHmtaeZMbTSfIowxXUCsQIYaC%2FvsIIA6MUu9Nx7DyZ2zuQtLd74BGaR8U9x0m4YT7QkWX4xQgtRpZl2siWPxVS1sjYcWsg4MRXRS2wJb9BgYZCM%2BJD2isUjFFBkj7osTgGT%2FQwzDnL%2B2U%2FGO2vmuAANGDrYaqjz8QXYY1OrHGLfwEShM3KpjutMxFuitVJ0hD7LO9AaDlCc86YqK%2BiYg60Iq%2Fhpb5v9ZfscWMcYnHODbMH7sSZ920Tos16uS2Cry%2FWJ0ftTZTNgcGX51ch4yJkAYsxXbboHWdZ7EP1VENGbc2asUSCpgR%2FghClaibOBJNQKCKqsnLDvHxR9x%2BC1yr47DThougmF6WKUoG5mFVNlYBHcZoSr4FXhDaP14lRXKUs4x4MsIGUwDAPQAzUKU1XVHUWULTfA4tAblsWcKBYkH1fSlgpMjjWtU2TF6pv%2F9BDZag9ppla6Q57vyD2td6mOYkojmGIpD1wAcXgsVQWNd5W1jYbpeetUdBUYWHHJJZtXIiKsUbF4k5nIcRJdemVlEghcyTpyEMiYJvggzxyOcr6it3Vkl%2FvjoBkODgY4TET8jGg8LTWvOm8SfFg%2B87m3XKNHu%2BOTrv7BuuTS5E%2B%2BUgz5BA9ASfy7MD%2Fd7AMNfTktLHPnDbjm5o2tmaxRy5NlEps9vSgSaFttHnaZpdyjNM3jKsb5ji7uo5xL%2BUYc6qRuOIsFpef2aZxemZbxrvObKRx8Qw7CXx%2FHzEde3E2h%2FatqcnUGFyzgKRCDrGPQ6NpX51H67ovsvbd9drNXetjuJHVzXwNj6oh2fxbOuQ6%2FznR8g8%3D).

### Frontend Web UI

- Show products
- Add products to cart
- Pay and send orders
- Sign-in / Register (Maybe one day)

### Orders API

- Provide products
- Provide user orders
- Handle order creation
- Send customer communications
- Sign-in / Register (Maybe one day)

### Logistic UI

- Show orders
- Change order status

### Main database

- Contains users and orders
