from http.server import BaseHTTPRequestHandler, HTTPServer
import json
from multiprocessing import connection
from urllib.parse import parse_qs, urlparse
import mysql.connector

connection = mysql.connector.connect(user='bHapticGame', 
                                         password='qwerty',
                                         host='127.0.0.1',
                                         database='leaderboard')
cursor = connection.cursor()

hostName = "localhost"
serverPort = 80

class LeaderboardServer(BaseHTTPRequestHandler):
    get_query = ("SELECT * FROM players "
                 "ORDER BY score DESC;")
    add_query = ("INSERT INTO players "
                 "VALUES (%s, %s) "
                 "ON DUPLICATE KEY "
                 "UPDATE score = IF(score < %s, %s, score);")
    gettop_query = ("SELECT * "
                    "FROM players "
                    "WHERE score >= %s;")
    

    def _set_response(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/html')
        self.end_headers()
    def do_GET(self):
        self._set_response()
        self.return_values()
        
    def return_values(self):
        url = urlparse(self.path)
        if(url.path == "/get"):
            results = self.get_leaderboard()
            self.wfile.write(bytes(json.dumps(results), "utf-8"))
        elif(url.path == "/getstr"):
            results = self.get_leaderboard()
            for val in results:
                self.wfile.write(bytes(val[0] + ": " + str(val[1]) + "\n", "utf-8"))
        elif(url.path == "/add"):
            query = parse_qs(url.query)
            name = query["name"][0]
            score = int(query["score"][0])
            self.add_to_leaderboard(name, score)
        elif(url.path == "/gettop"):
            query = parse_qs(url.query)
            score = int(query["score"][0])
            self.wfile.write(bytes(str(self.get_position(score)), "utf-8"))
            
    def get_leaderboard(self):
        cursor.execute(self.get_query)
        results = cursor.fetchall()
        return results
    def add_to_leaderboard(self, name, score):
        cursor.execute(self.add_query, (name, score, score, score))
        connection.commit()
    def get_position(self, score):
        cursor.execute(self.gettop_query, (score,))
        result = cursor.fetchall()
        return len(result) + 1

if __name__ == "__main__":        
    webServer = HTTPServer((hostName, serverPort), LeaderboardServer)
    print("Server started http://%s:%s" % (hostName, serverPort))

    try:
        webServer.serve_forever()
    except KeyboardInterrupt:
        pass

    webServer.server_close()
    cursor.close()
    connection.close()
    print("Server stopped.")
    
