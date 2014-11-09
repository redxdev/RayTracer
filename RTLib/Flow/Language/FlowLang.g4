grammar FlowLang;

@parser::header
{
	#pragma warning disable 3021
	using RTLib.Flow;
	using RTLib.Flow.Modules;
	using MathNet.Numerics.LinearAlgebra;
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

module returns [IFlowValue Value]
	:
		IDENT BLOCK_BEGIN
		{ Dictionary<string, IFlowValue> parameters = new Dictionary<string, IFlowValue>(); }
	(	first=module_parameter { parameters.Add($first.Key, $first.Value); }
		(
			ARG_SEPARATOR
			n=module_parameter
			{ parameters.Add($n.Key, $n.Value); }
		)*
	)?
		BLOCK_END
		{
			$Value = Scene.CreateModule($IDENT.text, parameters);
		}
	;

module_parameter returns [string Key, IFlowValue Value]
	:	IDENT EQUAL value { $Key = $IDENT.text; $Value = $value.Value; }
	;

tuple returns [IFlowValue Value]
	:	{ List<double> values = new List<double>(); }
		GROUP_BEGIN
	(	first=NUMBER { values.Add(double.Parse($first.text)); }
		(
			ARG_SEPARATOR
			n=NUMBER
			{ values.Add(double.Parse($n.text)); }
		)*
	)?
		GROUP_END
	{
		Vector<double> vector = Vector<double>.Build.Dense(values.Count);
		$Value = new GenericValue<Vector<double>>() {Value = vector};
	}
	;

value returns [IFlowValue Value]
	:
	(
		STRING { $Value = new GenericValue<string>() {Value = $STRING.text}; }
	|	NUMBER { $Value = new GenericValue<double>() {Value = double.Parse($NUMBER.text)};}
	|	VAR_SPECIFIER IDENT { $Value = new VariableValue() {Variable = $IDENT.text}; }
	|	tuple { $Value = $tuple.Value; }
	|	module { $Value = $module.Value; }
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