# TrendLoop
*********************************************************************
Admin Credentials:
Email = admin@trendloop.com
Password = Admin1234
***********************************************************************

TrendLoop: Redefining Affordable Luxury Fashion

Summary:
TrendLoop is a web application that bridges the gap between high-end fashion and affordability. It offers a seamless platform for discovering pre-loved designer pieces and luxury finds at unmatched prices. The mission of TrendLoop is to fosters a vibrant community of fashion enthusiasts committed to sustainability, creating a circular economy that elevates wardrobes while reducing waste.

Database Overview:
The database for TrendLoop is structured to support a robust and easy-extensible platform for affordable luxury fashion. Here's an overview of its main components:

*** User Management
The AspNetUsers table manages user details, including email, username, and optional attributes like AvatarUrl and SellerRating.
Role and claims management is handled via AspNetRoles, AspNetUserRoles, AspNetRoleClaims, and AspNetUserClaims for fine-grained authorization.

*** Product Listings
Products serves as the central table for managing items, with details such as name, description, price, images, and relationships to sellers (SellerId), buyers (BuyerId), categories, and brands.
Products have flexible attributes managed through AttributeTypes and AttributeValues, linked via ProductsAttributeValues.

*** Categories and Brands
Products are categorized under hierarchical structures (Categories and Subcategories) with soft-delete functionality (IsDeleted).
Brands enables filtering by luxury brand names, supporting active/inactive status (IsDeleted).
Extensibility and Customization
