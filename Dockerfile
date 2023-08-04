# Compile stage
FROM golang:1.17 AS build-env
ADD . /dockerdev
WORKDIR /dockerdev/cmd
RUN ls -la
RUN go build -o /out
# Final stage
FROM debian:buster
EXPOSE 8000
WORKDIR /
COPY --from=build-env /out /
CMD ["/out"]