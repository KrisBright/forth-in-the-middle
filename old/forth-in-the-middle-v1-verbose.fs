require unix/socket.fs

\ initial socket creation
\ myserver <- mitm <- outsideclient
\ left                right

create mybuf 1000 allot

create port-right
create server-right
create socket-right

create port-left
create socket-left

5900 port-right !
9500 port-left !

: gomitm
port-right @ create-server
cr ." done create-server" .s
server-right !

server-right @ 1 listen
cr ." done listen" .s

server-right @ accept-socket
cr ." done accept-socket" .s
socket-right !

\ got an incoming connection, now need to connect to the left side myserver

s" localhost" port-left @ open-socket
cr ." done open-socket"
socket-left !

\ now loop forever

25 0 do
  cr i . ." sleeping 2 secs"
  2000 ms
  socket-right @ mybuf 1000 read-socket
  dup 0> if
    cr ." <---- "
    2dup dump
    ." <---- "
    socket-left @ write-socket
  else
\    ." ." \ dot for progres
  then
  socket-left @ mybuf 1000 read-socket
  dup 0> if
    cr ." ----> "
    2dup dump
    ." ----> "
    socket-right @ write-socket
  else
\    ." ." \ dot for progres
  then
loop

;




