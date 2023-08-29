#!/bin/bash

# Script author: BytesCrafter
# Script site: https://www.bytescrafter.net
# Script date: 19-04-2020
# Script ver: 1.0
# Script use to install Fuxion stack on Debian 12
#--------------------------------------------------
# List function:
# 1. f_check_root: check to make sure script can be run by user root
# 2. f_update_os: update all the packages
# 3. f_install_lemp: funtion to install LEMP stack
# 4. f_sub_main: function use to call the main part of installation
# 5. f_main: the main function, add your functions to this place

# Function check user root
f_check_root() {
    if (($EUID == 0)); then
        # If user is root, continue to function f_sub_main
        f_sub_main
    else
        # If user not is root, print message and exit script
        echo "Fuxion NET: Please run this script by user root ."
        exit
    fi
}

# Function update os
f_update_os() {
    echo "Fuxion NET: Trying to update the system ..."
    echo ""
    sleep 1
    apt update
    apt upgrade -y
    echo ""
    sleep 1
}

# Installng nodejs, npm, and redis.
f_install_require() {
    echo "Fuxion NET: Installing Node and Redis ..."
    echo ""
    sleep 1
    apt install redis-server -y
    apt install curl software-properties-common -y
    curl -sL https://deb.nodesource.com/setup_20.x | sudo bash -
    apt install nodejs -y
    echo "USocketNet: NodeJS version is "
    node -v
    echo "USocketNet: NPM version is "
    npm -v
    echo ""
    sleep 1
}

# Installng nodejs, npm, and redis.
f_hardened_firewall() {
    echo "Fuxion NET: Hardening the firewall config..."
    echo ""
    sleep 1
    apt install sudo ufw net-tools vim -y
    sudo ufw allow 22
    sudo ufw allow 80
    sudo ufw allow 443
    sudo ufw allow 19090
    sudo ufw allow 6060
    sudo ufw allow 5050
    sudo ufw allow 4531
    sudo ufw allow 9090
    sudo ufw allow 1883
    sudo ufw enable -y
    sudo ufw reload
    echo ""
    sleep 1
}

# Function install LEMP stack
f_install_components() {
    # Install and start nginx
    echo ""
    echo "Fuxion NET: Installing NGINX ..."
    echo ""
    sleep 1
    apt install nginx -y
    systemctl enable nginx && systemctl restart nginx
    echo ""
    sleep 1

    # Increase max upload size on php.
    sed -i 's:# Basic Settings:client_max_body_size 24m;:g' /etc/nginx/nginx.conf

    # Create demo nginx vhost config file
    echo "Fuxion NET: Creating demo Nginx vHost config file ..."
    echo ""
    sleep 1r
    cat >/etc/nginx/sites-enabled/default <<"EOF"

# issue! php cannot handle the load and nginx config 
    # about worker connection and open file.
    # Traffic Load	        Average	    Heavy
    # Max Children	         25-35	    40-60
    # Process Idle Timeout	 100	    100-150
    # Max Requests	         200-350	400-600

# user  www-data;
worker_processes  2; #number of processor.
worker_rlimit_nofile 8192; #worker_connection x number of process.

events {
    worker_connections  4096;
}

http {
    include       mime.types;
    default_type  application/octet-stream;
    sendfile        on;
    #tcp_nopush     on;
    #keepalive_timeout  0;
    keepalive_timeout  65;
    #gzip  on;

    #region Fuxion NET - MASTER server
        upstream fuxion_master {
            ##ip_hash; #enable for sticky connection.
            #least_conn; #enable new conn on least traffic.
            server 0.0.0.0:19091;
            server 0.0.0.0:19092;
            #add multiple master server. 'server host:port+1'
        } server {
            listen          19090;
            listen          0.0.0.0:19090;
            server_name     _;

            location / {
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection "upgrade";
                proxy_http_version 1.1;
                # forwarder here.
                proxy_pass http://fuxion_master;
            }
        }
    #endregion

    #region Fuxion NET - CHAT server
        upstream fuxion_chat {
            #ip_hash; #enable for sticky connection.
            #least_conn; #enable new conn on least traffic.
            server 0.0.0.0:6061;
            server 0.0.0.0:6062;
            #add multiple chat server. 'server host:port+1'
        } server {
            listen          6060;
            listen          0.0.0.0:6060;
            server_name     _;

            location / {
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection "upgrade";
                proxy_http_version 1.1;
                proxy_pass http://fuxion_chat;
            }
        }
    #endregion

    #region Fuxion NET - VOICE server
        upstream fuxion_voice {
            #ip_hash; #enable for sticky connection.
            #least_conn; #enable new conn on least traffic.
            server 0.0.0.0:5051;
            server 0.0.0.0:5052;
            #add multiple voice server. 'server host:port+1'
        } server {
            listen          5050;
            listen          0.0.0.0:5050;
            server_name     _;

            location / {
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection "upgrade";
                proxy_http_version 1.1;
                proxy_pass http://fuxion_voice;
            }
        }
    #endregion

    #region Fuxion NET - WORLD server
        upstream fuxion_world {
            #ip_hash; #enable for sticky connection.
            #least_conn; #enable new conn on least traffic.
            server 0.0.0.0:4531;
            server 0.0.0.0:4532;
            #add multiple world server. 'server host:port+1'
        } server {
            listen          4530;
            listen          0.0.0.0:4530;
            server_name     _;

            location / {
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection "upgrade";
                proxy_http_version 1.1;
                proxy_pass http://fuxion_world;
            }
        }
    #endregion

    #region Fuxion NET - GAME server
        upstream fuxion_game {
            #ip_hash; #enable for sticky connection.
            #least_conn; #enable new conn on least traffic.
            server 0.0.0.0:9091;
            server 0.0.0.0:9092;
            #add multiple game server. 'server host:port+1'
        } server {
            listen          9090;
            listen          0.0.0.0:9090;
            server_name     _;

            location / {
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection "upgrade";
                proxy_http_version 1.1;
                proxy_pass http://fuxion_game;
            }
        }
    #endregion

        #region Fuxion NET - AUTO server
        upstream fuxion_cluster {
            ##ip_hash; #enable for sticky connection.
            #least_conn; #enable new conn on least traffic.
            server 0.0.0.0:1884;
            server 0.0.0.0:1885;
            #add multiple cluster server. 'server host:port+1'
        } server {
            listen          1883;
            listen          0.0.0.0:8080;
            server_name     _;

            location / {
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection "upgrade";
                proxy_http_version 1.1;
                # forwarder here.
                proxy_pass http://fuxion_cluster;
            }
        }
    #endregion
}

EOF

    # Restart nginx
    echo "Fuxion NET: Restarting Nginx..."
    echo ""
    sleep 1
    systemctl restart nginx
    sleep 1

}

# The sub main function, use to call neccessary functions of installation
f_sub_main() {
    f_update_os
    f_install_require
    f_install_components
    f_hardened_firewall
}

# The main function
f_main() {
    f_check_root
    f_sub_main
}
f_main

exit