extends Line2D


export var length = 20
var point = Vector2()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	global_position = Vector2(0,0)
	global_rotation = 0
	point = get_parent().get_node("Position2D").global_position
	add_point(point)
	while get_point_count()> length:
		remove_point(0)
