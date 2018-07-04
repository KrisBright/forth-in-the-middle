require unix/socket.fs   create buf 1400 allot
: rw ( socket-from socket-to direction -- socket-to socket-from )
  >r swap 2dup buf 1400 read-socket dup 0> 
  if cr r> emit 2dup dump rot write-socket else r> 2drop 2drop then ;
: mitm ( ip-addr ip-u port-server port-mitm -- )
  create-server dup 1 listen accept-socket >r open-socket r> swap
  begin 1 ms 60 rw 62 rw again ; \ usage: s" 127.0.0.1" 9500 5900 mitm
