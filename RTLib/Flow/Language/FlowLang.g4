grammar FlowLang;

@parser::header
{
	#pragma warning disable 3021
	using RTLib.Flow;
	using RTLib.Flow.Modules;
}

@parser::members
{
	protected const int EOF = Eof;

	public FlowScene Scene { get; set; }
}

@lexer::header
{
	#pragma warning disable 3021
}

@lexer::members
{
	protected const int EOF = Eof;
	protected const int HIDDEN = Hidden;
}

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
	:	IDENT value
		{
			Scene.AddVariable($IDENT.text, $value.Value);
		}
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

tuple returns [IFlowValue Value]
	:	GROUP_BEGIN
	(	value
		(ARG_SEPARATOR NUMBER)*
	)?
		GROUP_END
	;

value returns [IFlowValue Value]
	:
	(
		STRING { return new GenericValue<string>() {Value = $STRING.text};
	|	NUMBER { return new GenericValue<double>() {Value = double.Parse($NUMBER.text)};
	|	VAR_SPECIFIER IDENT { return new VariableValue() {Variable = $IDENT.text};
	|	tuple { return $tuple.Value; }
	|	module { return $module.Value; }
	)
	;

/*
 * Lexer Rules
 */

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