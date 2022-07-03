# Notification Email Service

This service is used to send notifications to the frontend application or to users in other ways when a analysis is
finished. It currently only sends an email, but can be extended with other notification methods. The current project
lacks real time feedback of the state of a running or finished analysis. It is advised to add for example SignalR to
this service to be able to send real time information to the frontend.

Adds a notification service to send mail to the avatar developers. Currently only has an post endpoint that accepts a
UserId and AvatarName in the body of the request that sends a simple mail to the user with that Id.

The mail service requires the connection string, the mail host url and mailing port to be set via environment variables.

The visual studio profiles in launchsettings.json assume a local SQL server running using integrated security. The mail
server is also on localhost with the port on 1025.

One way you could run this mail server locally is via MailDev. This is a simple SMTP server running locally on your PC
which captures the mails instead of sending them.
To run it on Docker use the command:

`docker run -p 1080:1080 -p 1025:1025 maildev/maildev`

When it is running you can view all mails sent to it on http://localhost:1080/#/

## Issues

- The maildev docker container is sometimes not able to start on Windows because the port is already in use. Changing
  the port fixes the issue.
