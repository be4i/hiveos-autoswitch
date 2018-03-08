#!/bin/bash
#
# Adjust fan speed automatically.
# This version by StuJordan
# Based on Version by DarkPhoinix
# Original script by Mitch Frazier

export DISPLAY=:0

# Target temperature for video card.
target_temp=65

# Value used to calculate the temperature range (+/- target_temp).
target_range=1

# Time to wait before re-checking.
sleep_time=10

# Minimum fan speed.
min_fanspeed=20

# Fan speed increment.
adj_fanspeed=2

if [ "$1" ]; then target_temp=$1; fi

target_temp_low=$(expr $target_temp - $target_range)
target_temp_high=$(expr $target_temp + $target_range)
first_run=true

while true
do
	cards_num=`nvidia-smi -L | wc -l`
	for ((i=0; i<$cards_num; i++))
	do
		temp_val=`nvidia-smi -i $i --query-gpu=temperature.gpu --format=csv,noheader`
		pwm_val=`nvidia-smi -i $i --query-gpu=fan.speed --format=csv,noheader | cut -d '%' -f 1 | tr -d '[:space:]'`
		
		if [ $first_run = true ]; then
			echo "Setting manual fan control for GPU${i}"
			result=`nvidia-settings -a [gpu:$i]/GPUFanControlState=1 | grep "assigned value 1"`
			test -z "$result" && echo "GPU${i} ${GPU_TEMP}°C -> Fan speed management is not supported" && continue
		fi
		
		echo "Current GPU${i} temp is $temp_val. Current pwm is $pwm_val"

		if [ $temp_val -gt $target_temp_high ]; then
		echo "GPU${i} temperature too high"

		# Temperature above target, see if the fan has any more juice.

		if [ $pwm_val -lt 100 ]; then
			echo "Increasing GPU${i} fan speed, temperature: $temp_val"
			pwm_val=$(expr $pwm_val + $adj_fanspeed)
			if [ $pwm_val -gt 100 ]; then pwm_val=100; fi
			nvidia-settings -a [fan:$i]/GPUTargetFanSpeed=$pwm_val > /dev/null 
		fi
		elif [ $temp_val -lt $target_temp_low ]; then
		
			# Temperature below target, lower the fan speed
			# if we're not already at the minimum.
			
			if [ $pwm_val -gt $min_fanspeed ]; then
				echo "Decreasing GPU${i} fan speed, temperature: $temp_val"
				pwm_val=$(expr $pwm_val - $adj_fanspeed)
				if [ $pwm_val -lt $min_fanspeed ]; then pwm_val=$min_fanspeed; fi
				nvidia-settings -a [fan:$i]/GPUTargetFanSpeed=$pwm_val > /dev/null
			fi
		fi
		
		#echo "GPU${i} ${temp_val}°C -> ${pwm_val}%"
	done
	first_run=false
	sleep $sleep_time
done