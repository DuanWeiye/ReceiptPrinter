# 
USB Receipt Printer APPs 

USB Receipt Printer And Raspberry Pi 


# How To Use 

### Download Raspbian on 
https://www.raspberrypi.org/downloads/raspbian/


### Write it to card 
`sudo dd if=<img file> of=/dev/<your card>`


### Enable SSH Server 
Create an empty file named "ssh" on root of your card


### Boot Raspberry Pi and login 
Default: pi/raspberry


### Setup, update and reboot 
`sudo raspi-config  
sudo apt update  
sudo apt upgrade  
sudo reboot`


### Install Python and Pip 
`sudo install python-dev python-usb python-pip libjpeg-dev`


### Install pyusb 
`git clone https://github.com/walac/pyusb.git  
python setup.py install`


### Upgrade Pip 
`sudo pip install --upgrade pip`


### Install libs 
`sudo pip install Pillow  
sudo pip install image  
sudo pip install python-escpos  
sudo pip install requests`


### Copy all files in \Raspiberry Pi\ to /home  
Also put font file (Font name in rplib.py).


### Get vendor ID, product Id, in_ep, out_ep 
`lsusb -v`


### Edit rplib.py 
Replace Vendor, Product, In, Out, Dpi and Width.


### Edit rpsync.py 
Replace your sync URL.


### Add crontab jobs 
`crontab -e  
*/2 * * * * sudo python /home/rpsync.py`


### Done, Enjoy! 
You can use WindowsAPP and AndroidAPP (Build after replace your own URL)  
Script template in \Raspiberry Pi 
