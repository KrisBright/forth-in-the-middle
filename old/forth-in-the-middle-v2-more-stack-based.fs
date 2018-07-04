require unix/socket.fs

\ myserver <- mitm <- outsideclient
\ left                right

create mybuf 1000 allot

: mitm ( port-L port-R -- )
  create-server
  dup 1 listen
  accept-socket
  swap
  s" localhost" 
  rot 
  open-socket
  swap
  mybuf
  ( socket-L socket-R buf ) 
  begin
    2000 ms
    2dup 1000 
    ( sL sR buf sR buf 1000 )
    read-socket
    ( sL sR buf buf u )
    dup 0> if
      cr cr ." <----" .s
      2dup dump
      4 pick write-socket
    else
      2drop
    then
    dup 3 pick swap 1000 read-socket
    dup 0> if
      cr cr ." ---->" .s
      2dup dump
      3 pick write-socket
    else
      2drop
    then
  again
;

: gomitm 9500 5900 mitm ;
\ 44 688
