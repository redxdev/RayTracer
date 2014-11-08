grammar FlowLang;

/*
 * Parser Rules
 */

compileUnit
	:	commands? EOF
	;

commands
	:	command+
	;

command
	:	cmd_context
	|	cmd_material
	|	cmd_scene
	;

cmd_context
	:	CMD_CONTEXT IDENT value
	;

cmd_material
	:	CMD_MATERIAL IDENT value
	;

cmd_scene
	:	CMD_SCENE IDENT value
	;

module
	:	
		IDENT BLOCK_BEGIN
	(	module_parameter
		(ARG_SEPARATOR module_parameter)*
	)?
		BLOCK_END
	;

module_parameter
	:	IDENT EQUAL value
	;

tuple
	:	GROUP_BEGIN
	(	value
		(ARG_SEPARATOR NUMBER)*
	)?
		GROUP_END
	;

value
	:
	(
		STRING
	|	NUMBER
	|	VAR_SPECIFIER IDENT
	|	tuple
	|	module
	)
	;

/*
 * Lexer Rules
 */

// command types
CMD_CONTEXT
	:	'context'
	;

CMD_MATERIAL
	:	'material'
	;

CMD_SCENE
	:	'scene'
	;

// language constructs
fragment ESCAPE_SEQUENCE
	:	'\\'
	(
		'\\'
	|	'"'
	|	'\''
	)
	;

STRING
	:
	(
		'"' ( ESCAPE_SEQUENCE | . )*? '"'
	|	'\'' ( ESCAPE_SEQUENCE | . )*? '\''
	)
	{
		Text = Text.Substring(1, Text.Length - 2)
				.Replace("\\\\", "\\")
				.Replace("\\\"", "\"")
				.Replace("\\\'", "\'");
	}
	;

NUMBER
	:	'-'?
	(
		[0-9]* '.' [0-9]+
	|	[0-9]+
	)
	;

IDENT
	:	[a-zA-Z_] [a-zA-Z0-9_.]*
	;

GROUP_BEGIN
	:	'('
	;

GROUP_END
	:	')'
	;

BLOCK_BEGIN
	:	'{'
	;

BLOCK_END
	:	'}'
	;

ARG_SEPARATOR
	:	','
	;

VAR_SPECIFIER
	:	'$'
	;

EQUAL
	:	'='
	;

WS
	:	[ \t\r\n] -> channel(HIDDEN)
	;

COMMENT
	:
	(	'//' ~[\r\n]*
	|	'/*' .*? '*/'
	)	-> channel(HIDDEN)
	;