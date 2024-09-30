# Rate Limiter using .NET 7

## Overview

This project is a simple web application built with .NET 7 that demonstrates the implementaition of rate limiting based on IP address and API Path.

### Why Use Rate Limiting?

- **Protects Your Server**: It prevents any user from sending too many requests to your server.
- **Enhances Security**:  It helps protect against attacks like brute force and denial of service (DoS).

### Rate Limiting Method

In this project, i used **Fixed Window Limiting** to manage request rates.

- **Fixed Window Limiting**: This approach sets limits based on unique keys, like the user's IP address and the requested path. When users reach their limit, they must wait until the next time slot to make more requests.
  
- **Rate Limit Settings**:
  - **PermitLimit**: The maximum number of requests allowed (e.g., 5).
  - **Window**: The time frame for counting requests (e.g., 10 seconds).
  - **QueueProcessingOrder**: If requests are queued, the oldest ones will be processed first.
  - **QueueLimit**: Setting this to 0 means that once the limit is reached, no additional requests will be queued. Users will receive a `429 Too Many Requests` response after reached the limit.

## `app.UseRateLimiter();`

This line turns on the rate limiting middleware across your .NET application globall.
