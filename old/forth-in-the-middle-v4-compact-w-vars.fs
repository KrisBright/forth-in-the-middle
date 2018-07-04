require unix/socket.fs
9500 value port0   5900 value port1   0 value sock0   0 value sock1
create buf 999 allot
: mitm ." server:" port0 . ." <- mitm:" port1 . ." <- client"
  port1 create-server dup 1 listen accept-socket to sock1 ."  - client ok"
  s" localhost" port0 open-socket to sock0 ."  - server ok"
  begin 1 ms 
    sock0 buf 999 read-socket dup 0> 
    if cr ." --->" 2dup dump sock1 write-socket else 2drop then
    sock1 buf 999 read-socket dup 0> 
    if cr ." <---" 2dup dump sock0 write-socket else 2drop then
  again
; 
mitm \ 14 552
